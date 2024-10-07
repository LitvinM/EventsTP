import React, {ChangeEvent, useEffect, useLayoutEffect, useState} from "react";
import {IEvent, IParticipant} from "./App"
import EventCard from "./EventCard";
import {Link, useNavigate} from "react-router-dom"
import Cookies from "js-cookie";

const MyEvents = () => {
    const base : IEvent[] = [];

    const [events, setEvents] = useState(base);

    const [selectedCategory, setSelectedCategory] = useState<string>("");

    const defaultFetch = () => {
        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => response.json())
            .then((data : IEvent[]) => {
                const email = Cookies.get("email");
                let es = data.filter(event =>
                    event.participants.some(participant => participant.email === email)
                );

                setEvents(es)
            })
            .catch(() => navigate('/register'));
    }

    const navigate = useNavigate()
    useEffect(() => {

        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => response.json())
            .then((data : IEvent[]) => {
                const email = Cookies.get("email");
                if(selectedCategory === "" || selectedCategory === "Category")
                {
                    defaultFetch()
                }
                let es = data.filter(event =>
                    event.category === selectedCategory && event.participants.some(participant => participant.email === email)
                );

                setEvents(es)
            })
            .catch(() => navigate('/register'));
    }, [selectedCategory])

    useLayoutEffect(() => {
        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => response.json())
            .then((data : IEvent[]) => {
                const email = Cookies.get("email");
                let es = data.filter(event =>
                    event.participants.some(participant => participant.email === email)
                );

                setEvents(es)
            })
            .catch(() => navigate('/register'));
    }, []);

    const [searchTerm, setSearchTerm] = useState('');

    const handleClick = () => {
        if(searchTerm !== '') {
            fetch(`http://localhost:5175/api/Event/search/${searchTerm}`, {
                credentials: 'include'
            })
                .then(response => response.json())
                .then(data => setEvents(data))
                .catch()
        }
        else{
            fetch('http://localhost:5175/api/Event',{
                credentials: 'include'
            })
                .then((response) => response.json())
                .then((data) => setEvents(data))
                .catch(() => navigate('/register'));
        }
    }

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(event.target.value);
    };

    return (
        <div>
            <div className={"header"}>
                <div/>
                <h1><Link className={"Link"} to="/home">Home</Link></h1>
                <h2><Link className={"Link"} to="/my-events">My Events</Link></h2>
                <div className="search-container">
                    <input
                        type="text"
                        placeholder="Search..."
                        className="search-input"
                        value={searchTerm}
                        onChange={handleInputChange}
                    />
                    <button type="button" className="search-button" onClick={handleClick}>
                        Search
                    </button>
                </div>
                <div className="filter-container">
                    <select
                        value={selectedCategory}
                        onChange={(e) => setSelectedCategory(e.target.value)}
                        className="filter-select"
                    >
                        <option value="">Category</option>
                        <option value="Concert">Concert</option>
                        <option value="Football">Football</option>
                        <option value="Art Gallery">Art Gallery</option>
                    </select>
                </div>
            </div>
            <div className={"cards-container"}>
                <div className={"title"}>
                    <span>Name</span>
                    <span>Category</span>
                    <span>Place</span>
                    <span>Time and Date</span>
                    <span>Amount</span>
                </div>
                {
                    events.map(event => {

                        const onClickHandle = () => {
                            navigate(`/event/${event.id}`)
                        }

                        return (

                            <EventCard key={event.id} onClickHandle={onClickHandle} event={event}/>
                        )
                    })
                }
            </div>
        </div>
    )
}

export default MyEvents