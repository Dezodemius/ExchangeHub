import { useState } from 'react'
import { api } from '../api'


export default function Register() {
    const [name, setName] = useState('')
    const [password, setPassword] = useState('')
    const [message, setMessage] = useState('')


    const submit = async () => {
        try {
            await api.post('/api/auth/register', { name, password })
            setMessage('Registered!')
        } catch (e: any) {
            setMessage('Error: ' + e.message)
        }
    }


    return (
        <div>
            <h2>Register</h2>
            <input placeholder="name" value={name} onChange={e => setName(e.target.value)} /><br />
            <input placeholder="password" type="password" value={password} onChange={e => setPassword(e.target.value)} /><br />
            <button onClick={submit}>Register</button>
            <div>{message}</div>
        </div>
    )
}