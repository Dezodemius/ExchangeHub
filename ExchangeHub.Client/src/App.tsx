import { Routes, Route, Link } from 'react-router-dom'
import Register from './pages/Register'
import Login from './pages/Login'
import AllCurrencies from './pages/AllCurrencies'
import Favorites from './pages/Favorites'


export default function App() {
    return (
        <div style={{ padding: 20 }}>
            <nav style={{ display: 'flex', gap: 10, marginBottom: 20 }}>
                <Link to="/register">Register</Link>
                <Link to="/login">Login</Link>
                <Link to="/currencies">All Currencies</Link>
                <Link to="/favorites">Favorites</Link>
            </nav>


            <Routes>
                <Route path="/register" element={<Register />} />
                <Route path="/login" element={<Login />} />
                <Route path="/currencies" element={<AllCurrencies />} />
                <Route path="/favorites" element={<Favorites />} />
            </Routes>
        </div>
    )
}