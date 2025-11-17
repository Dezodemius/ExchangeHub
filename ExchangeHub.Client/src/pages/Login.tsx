import { useState } from 'react'
import { api, setToken } from '../api'


export default function Login() {
    const [name, setName] = useState('')
    const [password, setPassword] = useState('')
    const [message, setMessage] = useState('')


    const submit = async () => {
        try {
            const res = await api.post('/api/auth/login', { name, password })
            setToken(res.data.token)
            localStorage.setItem('token', res.data.token)
            setMessage('Logged in!')
        } catch (e: any) {
            setMessage('Error: ' + e.message)
        }
    }


    return (
        <div>
            <h2>Login</h2>
            <input placeholder="name" value={name} onChange={e => setName(e.target.value)} /><br />
            <input placeholder="password" type="password" value={password} onChange={e => setPassword(e.target.value)} /><br />
            <button onClick={submit}>Login</button>
            <div>{message}</div>
        </div>
    )
}