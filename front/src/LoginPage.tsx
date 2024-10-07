import React, {useEffect, useState} from 'react';
import {Link, useNavigate} from "react-router-dom";
import Cookies from 'js-cookie';

const LoginPage = () => {
    useEffect(() => {
        Cookies.remove("email")
        Cookies.remove("cook")
    }, [])

    const [participant, setParticipant] = useState({
        email: '',
        password: ''
    });
    const [errorText, setErrorText] = useState("");
    const navigate = useNavigate();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setParticipant({ ...participant, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const req = {...participant}

        if(req.email == '' || req.password == ''){
            return
        }

        fetch('http://localhost:5175/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(req),
            credentials: 'include'
        }).then((response) =>{
            if(response.status === 401 || response.status > 204) throw response.status
            return response.json();
        }).then(() => {
            Cookies.set("email", req.email)

            if(req.email == "admin@example.com"){
                navigate('/home-admin')
                return
            }
            navigate('/home')

        }).catch(() => {
            setErrorText("Login exception. Wrong email or password");
        });
    };

    return (
        <form className="authForm" onSubmit={handleSubmit}>
            <h2>Login</h2>
            <label className="authLabel">
                <p>Email</p>
                <input type="email" name="email" value={participant.email} onChange={handleInputChange} />
            </label>
            <label className="authLabel">
                <p>Password</p>
                <input type="password" name="password" value={participant.password} onChange={handleInputChange} />
            </label>
            <p>{errorText}</p>
            <Link className={"lr-link"} to={"/register"}>Register</Link>
            <button type="submit"><span>Login</span></button>
        </form>
    );
};

export default LoginPage;