import {IEvent, IParticipant} from "./App";
import {useNavigate, useParams} from 'react-router-dom';
import React, {useEffect, useState} from "react";
import Header from "./Header";
import Cookies from "js-cookie";

const EventPage = () => {
    const { id } = useParams<{ id: string }>();
    const [event, setEvent] = useState<IEvent>();
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();
    const [participant, setParticipant] = useState<IParticipant>({
        dateOfBirth: "",
        email: "",
        events: [],
        id: "",
        name: "",
        password: "",
        surname: ""
    })

    useEffect(() => {
        fetch(`http://localhost:5175/api/Event/${id}`, {
            credentials: 'include'
        })
            .then(response => {
                if (response.status === 401) {
                    throw "Unauthorized";
                }
                return response.json();
            })
            .then(data => {
                setEvent(data);
                setLoading(false);
            })
            .catch(error => {
                if(error == "Unauthorized") navigate('/register')
                setError(error.message);
                setLoading(false);
            });
    }, [id]);

    useEffect(() => {
        if(event !== null){
            let p  = {...participant}
            fetch(`http://localhost:5175/api/event/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(p.email),
                credentials: 'include'
            }).catch()
        }
    }, [event])

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!event) {
        return <div>No event found</div>;
    }

    const handleClick = () => {
        if(event.participants.length >= event.participantsMaxAmount) return
        // @ts-ignore
        const email = Cookies.get('email').toString()
        let contains = false;
        event.participants.forEach(p => {
            if(p.email === email) contains = true;
        })
        if(!contains){

            fetch("http://localhost:5175/api/participant/byemail",{
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(email),
            })
                .then((response) => response.json())
                .then((data) => {
                    setParticipant(data)
                    let e = {...event}
                    e.participants.push(data);
                    setEvent(e)
                })
        }
    }

    return (
        <div>
            <Header/>
            <div className="event-page">
                <h1>{event.name}</h1>
                <img src={event.image} alt={event.name} className="event-image" />
                <p><strong>Description:</strong> {event.description}</p>
                <p><strong>Date & Time:</strong> {new Date(event.timeAndDate).toLocaleString()}</p>
                <p><strong>Place:</strong> {event.place}</p>
                <p><strong>Category:</strong> {event.category}</p>
                <p><strong>Max Participants:</strong> {event.participantsMaxAmount}</p>
                <p><strong>Current Participants:</strong> {event.participants.length}</p>
                <button className={'subscribe-to-the-event'} onClick={handleClick}><span>Subscribe to the event</span></button>
                <h2>Participants List</h2>
                <ul className="participants-list">
                    {event.participants.map(participant => (
                        <li key={participant.id}>
                            {participant.name} {participant.surname} - {participant.email}
                        </li>
                    ))}
                </ul>
            </div>
        </div>

    );
};

export default EventPage;