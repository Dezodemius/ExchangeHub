import React, { useState } from 'react';
import { api, setToken } from '../api';

type AuthMode = 'login' | 'register';

interface AuthPageProps {
    onAuthSuccess: () => void;
}

const Auth: React.FC<AuthPageProps> = ({ onAuthSuccess }) => {
    const [mode, setMode] = useState<AuthMode>('login');
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const handleSubmit = async () => {
        setLoading(true);
        setMessage(null);

        try {
            if (mode === 'register') {
                await api.post('/api/auth/register', { name, password });
                setMessage('Регистрация успешна. Теперь можно войти.');
                setMode('login');
            } else {
                const res = await api.post('/api/auth/login', { name, password });
                const token = res.data.token as string;
                setToken(token);
                setMessage('Успешный вход!');
                onAuthSuccess();
            }
        } catch (e: any) {
            setMessage(e.response?.data?.message ?? `Ошибка: ${e.message}`);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="page page--centered">
            <div className="card auth-card">
                <div className="auth-card__tabs">
                    <button
                        className={`auth-card__tab ${mode === 'login' ? 'auth-card__tab--active' : ''}`}
                        onClick={() => setMode('login')}
                    >
                        Вход
                    </button>
                    <button
                        className={`auth-card__tab ${mode === 'register' ? 'auth-card__tab--active' : ''}`}
                        onClick={() => setMode('register')}
                    >
                        Регистрация
                    </button>
                </div>

                <div className="auth-card__body">
                    <h2>{mode === 'login' ? 'Добро пожаловать' : 'Создать аккаунт'}</h2>

                    <div className="form-field">
                        <label>Имя пользователя</label>
                        <input
                            type="text"
                            placeholder="Введите имя"
                            value={name}
                            onChange={e => setName(e.target.value)}
                        />
                    </div>

                    <div className="form-field">
                        <label>Пароль</label>
                        <input
                            type="password"
                            placeholder="Введите пароль"
                            value={password}
                            onChange={e => setPassword(e.target.value)}
                        />
                    </div>

                    <button
                        className="btn btn--primary btn--full"
                        onClick={handleSubmit}
                        disabled={loading || !name || !password}
                    >
                        {loading
                            ? 'Обработка...'
                            : mode === 'login'
                                ? 'Войти'
                                : 'Зарегистрироваться'}
                    </button>

                    {message && <div className="form-message">{message}</div>}
                </div>
            </div>
        </div>
    );
};

export default Auth;
