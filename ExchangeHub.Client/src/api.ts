import axios from 'axios'

const API_URL = import.meta.env.VITE_API_URL;

export const api = axios.create({ baseURL: API_URL })

export const setToken = (token: string | null) => {
    if (token) {
        api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        localStorage.setItem('token', token);
    } else {
        delete api.defaults.headers.common['Authorization'];
        localStorage.removeItem('token');
    }
};

const existingToken = localStorage.getItem('token');
if (existingToken) {
    setToken(existingToken);
}