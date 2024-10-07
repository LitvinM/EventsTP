import {IEvent} from "./App";
import {Link, useNavigate, useParams} from 'react-router-dom';
import React, {useEffect, useState} from "react";

const AdminEventPage = () => {
    const { id } = useParams<{ id: string }>();
    const [event, setEvent] = useState<IEvent>({
        category: "",
        description: "",
        id: "",
        image: "",
        name: "",
        participants: [],
        participantsMaxAmount: 0,
        place: "",
        timeAndDate: ""
    });
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const [fileName, setFileName] = useState<string>('No file chosen');

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            setFileName(file.name);
            const reader = new FileReader();
            reader.onloadend = () => {
                if (reader.result) {
                    setEvent((prevEvent) => ({
                        ...prevEvent,
                        image: reader.result as string,
                    }));
                }
            };
            reader.readAsDataURL(file);
        }
    };

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

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    if (!event) {
        return <div>No event found</div>;
    }

    const handleSave = () => {
        let e = {...event}
        console.log(e)
        fetch(`http://localhost:5175/api/admin/event/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(e),
            credentials: 'include'
        }).then(() => {navigate('/home-admin')}).catch()

    }

    const handleDelete = () => {
        fetch(`http://localhost:5175/api/admin/event/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        }).then(() => {navigate('/home-admin')}).catch()
    }

    return (
        <div>
            <div className={"header"}>
                <div/>
                <h1><Link className={"Link"} to="/home-admin">Home</Link></h1>
                <h2><Link className={"Link"} to="/add-event">Add event</Link></h2>
                <div/>
            </div>
            <div className="event-page">
                    <p>
                        <strong>Name:</strong>
                        <input
                            type="text"
                            value={event.name}
                            onChange={(e) => setEvent({ ...event, name: e.target.value })}
                        />
                    </p>

                    <p>
                        <strong>Image:</strong>
                        <div className="file-input-container">

                            <label htmlFor="file-upload" className="custom-file-upload">
                                Choose file
                            </label>
                            <input
                                id="file-upload"
                                type="file"
                                accept="image/*"
                                onChange={handleFileChange}
                            />
                            <span className="file-name">{fileName}</span>
                        </div>
                    </p>

                    <p>
                        <strong>Description:</strong>
                        <textarea
                            value={event.description}
                            onChange={(e) => setEvent({ ...event, description: e.target.value })}
                        />
                    </p>

                    <p>
                        <strong>Date & Time:</strong>
                        <input
                            type="datetime-local"
                            value={new Date(event.timeAndDate).toISOString().slice(0, 16)}
                            onChange={(e) => setEvent({ ...event, timeAndDate: e.target.value })}
                        />
                    </p>

                    <p>
                        <strong>Place:</strong>
                        <input
                            type="text"
                            value={event.place}
                            onChange={(e) => setEvent({ ...event, place: e.target.value })}
                        />
                    </p>

                    <p>
                        <strong>Category:</strong>
                        <select
                            value={event.category}
                            onChange={(e) => setEvent({ ...event, category: e.target.value })}
                        >
                            <option value="Concert">Concert</option>
                            <option value="Football">Football</option>
                            <option value="Art Gallery">Art Gallery</option>
                        </select>
                    </p>

                    <p>
                        <strong>Max Participants:</strong>
                        <input
                            type="number"
                            value={event.participantsMaxAmount}
                            onChange={(e) => setEvent({ ...event, participantsMaxAmount: Number(e.target.value) })}
                        />
                    </p>

                    <button type="submit" onClick={handleSave}>Save Changes</button>
                    <button type="submit" className={"red-button"} onClick={handleDelete}>Delete event</button>

                <p><strong>Current Participants:</strong> {event.participants.length}</p>

                <h2>Participants List</h2>
                <ul className="participants-list">
                    {event.participants.map((participant) => (
                        <li key={participant.id}>
                            {participant.name} {participant.surname} - {participant.email}
                        </li>
                    ))}
                </ul>
            </div>

        </div>

    );
};

export default AdminEventPage;