import React, { useEffect, useState } from 'react';
import { api } from '../api';
import { Currency } from '../types';
import { HeartButton } from '../Components/HeartButton';

const CurrenciesPage: React.FC = () => {
    const [currencies, setCurrencies] = useState<Currency[]>([]);
    const [loading, setLoading] = useState(true);
    const [message, setMessage] = useState<string | null>(null);
    const [updatingId, setUpdatingId] = useState<number | null>(null);

    const load = async () => {
        setLoading(true);
        setMessage(null);
        try {
            const res = await api.get<Currency[]>('/api/finance/all');
            setCurrencies(res.data);
        } catch (e: any) {
            setMessage(e.response?.data?.message ?? `Ошибка загрузки: ${e.message}`);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        load();
    }, []);

    const toggleFavorite = async (id: number) => {
        setUpdatingId(id);
        setMessage(null);

        try {
            await api.post('/api/favorites/add', { CurrencyId: id });

            setCurrencies(prev =>
                prev.map(c =>
                    c.id === id ? { ...c, isFavorite: !c.isFavorite } : c
                )
            );
        } catch (e: any) {
            setMessage(e.response?.data?.message ?? `Ошибка обновления: ${e.message}`);
        } finally {
            setUpdatingId(null);
        }
    };

    return (
        <div className="page">
            <div className="page-header">
                <h1>Все курсы валют</h1>
                <button className="btn btn--ghost" onClick={load}>
                    Обновить
                </button>
            </div>

            {loading && <div className="info">Загрузка курсов...</div>}
            {message && <div className="info info--error">{message}</div>}

            {!loading && currencies.length === 0 && (
                <div className="info">Пока нет данных по валютам.</div>
            )}

            <div className="list">
                {currencies.map(c => (
                    <div className="list-item" key={c.id}>
                        <div className="list-item__main">
                            <div className="list-item__title">{c.name}</div>
                            <div className="list-item__subtitle">Курс: {c.rate}</div>
                        </div>
                        <div className="list-item__actions">
                            <HeartButton
                                active={!!c.isFavorite}
                                onClick={() => toggleFavorite(c.id)}
                            />
                            {updatingId === c.id && (
                                <span className="list-item__spinner">...</span>
                            )}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default CurrenciesPage;
