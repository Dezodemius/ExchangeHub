import React, { useState, useEffect } from 'react';
import Auth from './pages/Auth';
import CurrenciesPage from './pages/Currencies';
import FavoritesPage from './pages/Favorites';
import { setToken } from './api';
import './App.css';

type Page = 'auth' | 'all' | 'favorites';

const App: React.FC = () => {
    const [page, setPage] = useState<Page>('auth');
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

    useEffect(() => {
        const token = localStorage.getItem('token');
        setIsAuthenticated(!!token);
        if (!token) {
            setPage('auth');
        } else {
            setPage('all');
        }
    }, []);

    const handleLogout = () => {
        setToken(null);
        setIsAuthenticated(false);
        setPage('auth');
    };

    const handleAuthSuccess = () => {
        setIsAuthenticated(true);
        setPage('all');
    };

    return (
        <div className="app">
            <header className="topbar">
                <div className="topbar__brand">
                    <span className="topbar__logo">💱</span>
                    <span className="topbar__title">Exchange Hub</span>
                </div>

                {isAuthenticated && (
                    <nav className="topbar__nav">
                        <button
                            className={`topbar__link ${page === 'all' ? 'topbar__link--active' : ''}`}
                            onClick={() => setPage('all')}
                        >
                            Все валюты
                        </button>
                        <button
                            className={`topbar__link ${page === 'favorites' ? 'topbar__link--active' : ''}`}
                            onClick={() => setPage('favorites')}
                        >
                            Избранные
                        </button>
                    </nav>
                )}

                <div className="topbar__right">
                    {isAuthenticated ? (
                        <button className="btn btn--ghost" onClick={handleLogout}>
                            Выйти
                        </button>
                    ) : (
                        <button
                            className="btn btn--ghost"
                            onClick={() => setPage('auth')}
                        >
                            Войти
                        </button>
                    )}
                </div>
            </header>

            <main className="app__content">
                {!isAuthenticated && <Auth onAuthSuccess={handleAuthSuccess} />}
                {isAuthenticated && page === 'all' && <CurrenciesPage />}
                {isAuthenticated && page === 'favorites' && <FavoritesPage />}
            </main>
        </div>
    );
};

export default App;
