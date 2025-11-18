import React from 'react';

interface HeartButtonProps {
    active: boolean;
    onClick: () => void;
}

export const HeartButton: React.FC<HeartButtonProps> = ({ active, onClick }) => {
    return (
        <button
            className={`heart-button ${active ? 'heart-button--active' : ''}`}
            onClick={onClick}
            aria-label={active ? 'Удалить из избранного' : 'Добавить в избранное'}
        >
            {active ? '♥' : '♡'}
        </button>
    );
};
