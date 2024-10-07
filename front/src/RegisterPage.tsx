import React, { useState } from 'react';
import {Link, useNavigate} from "react-router-dom";

const RegisterPage = () => {
    const [user, setUser] = useState({
        name: '',
        surname: '',
        dateOfBirth: '',
        email: '',
        password: ''
    });

    const navigate = useNavigate();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const req = {...user};
        if(req.dateOfBirth == '' || req.name == '' ||
            req.surname == '' || req.email == '' || req.password == ''){
            return
        }
        fetch('http://localhost:5175/api/Participant', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },body: JSON.stringify(req)
        }).then((response) => {
            if(response.status > 204) throw "User with this email already exists"
            navigate('/login')
        }).catch((error) => console.error('Ошибка:', error));
    };

    return (
        <form className="authForm" onSubmit={handleSubmit}>
            <h2>Register</h2>
            <label className="authLabel">
                <p>Name</p>
                <input type="text" name="name" value={user.name} onChange={handleInputChange} />
            </label>
            <label className="authLabel">
                <p>Surname</p>
                <input type="text" name="surname" value={user.surname} onChange={handleInputChange} />
            </label>
            <label className="authLabel">
                <p>Date of Birth</p>
                <input type="date" name="dateOfBirth" value={user.dateOfBirth} onChange={handleInputChange} />
            </label>
            <label className="authLabel">
                <p>Email</p>
                <input type="email" name="email" value={user.email} onChange={handleInputChange} />
            </label>
            <label className="authLabel">
                <p>Password</p>
                <input type="password" name="password" value={user.password} onChange={handleInputChange} />
            </label>
            <Link className={"lr-link"} to={"/login"}>Log in</Link>
            <button type="submit"><span>Register</span></button>
        </form>
    );
};

export default RegisterPage;