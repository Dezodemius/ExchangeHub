import React, { useEffect, useState } from 'react';
import { api } from '../api';
import { Currency } from '../types';
import { HeartButton } from '../Components/HeartButton';

const FavoritesPage: React.FC = () => {
    const [favorites, setFavorites] = useState<Currency[]>([]);
    const [loading, setLoading] = useState(true);
    const [message, setMessage] = useState<string | null>(null);
    const [updatingId, setUpdatingId] = useState<number | null>(null);

    const load = async () => {
        setLoading(true);
        setMessage(null);
        try {
            const res = await api.get<Currency[]>('/api/finance/favorites');
            setFavorites(res.data.map(c => ({ ...c, isFavorite: true })));
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
            await api.post('/api/favorites/remove', { currencyId: id });

            setFavorites(prev => prev.filter(c => c.id !== id));
        } catch (e: any) {
            setMessage(
                e.response?.data?.message ??
                `Ошибка при удалении: ${e.message}`
            );
        } finally {
            setUpdatingId(null);
        }
    };

    return (
        <div className="page">
            <div className="page-header">
                <h1>Избранные валюты</h1>
                <button className="btn btn--ghost" onClick={load}>
                    Обновить
                </button>
            </div>

            {loading && <div className="info">Загрузка избранных...</div>}
            {message && <div className="info info--error">{message}</div>}
            {!loading && favorites.length === 0 && (
                <div className="info">У вас пока нет избранных валют.</div>
            )}

            <div className="list">
                {favorites.map(c => (
                    <div className="list-item" key={c.id}>
                        <div className="list-item__main">
                            <div className="list-item__title">{c.name}</div>
                            <div className="list-item__subtitle">
                                Курс: {c.rate}
                            </div>
                        </div>

                        <div className="list-item__actions">
                            <HeartButton
                                active={true}
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


export default FavoritesPage;
