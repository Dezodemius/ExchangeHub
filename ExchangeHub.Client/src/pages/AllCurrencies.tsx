import { useState, useEffect } from 'react'
import { api } from '../api'
import { Currency } from '../types'


export default function AllCurrencies() {
    const [currencies, setCurrencies] = useState<Currency[]>([])
    const [message, setMessage] = useState('')


    useEffect(() => {
        api.get('/api/finance/currencies').then(r => setCurrencies(r.data))
    }, [])


    const addFavorite = async (id: number) => {
        try {
            await api.post(`/api/user/favorites/${id}`)
            setMessage('Added!')
        } catch (e: any) {
            setMessage('Error: ' + e.message)
        }
    }


    return (
        <div>
            <h2>All Currencies</h2>
            {currencies.map(c => (
                <div key={c.id}>
                    {c.name} — {c.rate}
                    <button onClick={() => addFavorite(c.id)}>Add to favorites</button>
                </div>
            ))}
            <div>{message}</div>
        </div>
    )
}