import { useState, useEffect } from 'react'
import { api } from '../api'
import { Currency } from '../types'


export default function Favorites() {
    const [favorites, setFavorites] = useState<Currency[]>([])


    useEffect(() => {
        api.get('/api/user/favorites').then(r => setFavorites(r.data))
    }, [])


    return (
        <div>
            <h2>Favorites</h2>
            {favorites.map(c => (
                <div key={c.id}>
                    {c.name} — {c.rate}
                </div>
            ))}
        </div>
    )
}