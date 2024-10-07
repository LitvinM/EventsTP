import React, {useLayoutEffect, useState} from "react";
import {IEventRequest} from "./App";
import {Link, useNavigate} from "react-router-dom";
import Header from "./Header";


const AddPage = () => {
    const baseEvent: IEventRequest = {
        timeAndDate: '',
        category: '',
        description: '',
        image: '',
        name: '',
        participantsMaxAmount: 0,
        place: ''
    };
    const [newEvent, setNewEvent] = useState<IEventRequest>(baseEvent);

    const navigate = useNavigate();

    useLayoutEffect(() => {
        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => {
                if(response.status === 401) throw "Unauthorized"
            })
            .catch(() => navigate('/register'));
    }, []);

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = event.currentTarget;
        setNewEvent((prevEvent) => ({
            ...prevEvent,
            [name]: value,
        }));
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const e = {...newEvent}
        e.timeAndDate += ":00.000Z"

        fetch('http://localhost:5175/api/admin/event', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(e),
            credentials: 'include'
        }).then(() => navigate("/home-admin")).catch((error) => console.error('Ошибка:', error));

    };
    const [fileName, setFileName] = useState<string>('No file chosen');

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            setFileName(file.name);
            const reader = new FileReader();
            reader.onloadend = () => {
                if (reader.result) {
                    setNewEvent((prevEvent) => ({
                        ...prevEvent,
                        image: reader.result as string,
                    }));
                }
            };
            reader.readAsDataURL(file);
        }
    };
    return (
        <div>
            <div className={"header"}>
                <div/>
                <h1><Link className={"Link"} to="/home-admin">Home</Link></h1>
                <h2><Link className={"Link"} to="/add-event">Add event</Link></h2>
                <div className="search-container">
                    <input
                        type="text"
                        placeholder="Search..."
                        className="search-input"
                    />
                    <button type="button" className="search-button">
                        Search
                    </button>
                </div>
                <div/>
            </div>
            <form className="addPage" onSubmit={handleSubmit}>
                <label className="addLabel">
                    <p>Name</p>
                    <input type="text" name="name" value={newEvent.name} onChange={handleInputChange} />
                </label>
                <label className="addLabel">
                    <p>Description</p>
                    <input type="text" name="description" value={newEvent.description} onChange={handleInputChange} />
                </label>
                <label className="addLabel">
                    <p>Time and date</p>
                    <input type="datetime-local" name="timeAndDate" value={newEvent.timeAndDate} onChange={handleInputChange} />
                </label>
                <label className="addLabel">
                    <p>Place</p>
                    <input type="text" name="place" value={newEvent.place} onChange={handleInputChange} />
                </label>
                <label className="addLabel">
                    <p>Category</p>
                    <select
                        name="category"
                        value={newEvent.category}
                        onChange={handleInputChange}
                        className="filter-select"
                    >
                        <option value="">Category</option>
                        <option value="Concert">Concert</option>
                        <option value="Football">Football</option>
                        <option value="Art Gallery">Art Gallery</option>
                    </select>
                </label>
                <label className="addLabel">
                    <p>Max amount of participants</p>
                    <input type="number" name="participantsMaxAmount" value={newEvent.participantsMaxAmount} onChange={handleInputChange} />
                </label>
                <div className="file-input-container">
                    <label htmlFor="file-upload" className="custom-file-upload">
                        Choose image
                    </label>
                    <input
                        id="file-upload"
                        type="file"
                        accept="image/*"
                        onChange={handleFileChange}
                    />
                    <span className="file-name">{fileName}</span>
                </div>
                <button type="submit"><span>Submit</span></button>
            </form>
        </div>

    );
}

export default AddPage;