import React, {ChangeEvent, useEffect, useLayoutEffect, useState} from "react";
import {IEvent} from "./App"
import EventCard from "./EventCard";
import {Link, useNavigate} from "react-router-dom"
import Cookies from "js-cookie";

const StartAdmin = () => {
    const base : IEvent[] = [];

    const [events, setEvents] = useState(base);
    const [selectedCategory, setSelectedCategory] = useState<string>("");

    const navigate = useNavigate()
    useEffect(() => {

        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => response.json())
            .then((data : IEvent[]) => {
                if(selectedCategory === "" || selectedCategory === "Category")
                {
                    defaultFetch()
                    return;
                }
                let es = data.filter(event =>
                    event.category === selectedCategory
                );

                setEvents(es)
            })
            .catch(() => navigate('/register'));
    }, [selectedCategory])

    useLayoutEffect(() => {
        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => {
                if(response.status === 403 || response.status === 401) throw "forbidden"
                return response.json()
            })
            .then((data) => setEvents(data))
            .catch(() => navigate('/register'));
    }, []);

    const [searchTerm, setSearchTerm] = useState('');

    const handleClick = () => {
        if(searchTerm !== '') {
            fetch(`http://localhost:5175/api/Event/search/${searchTerm}`, {
                credentials: 'include'
            })
                .then(response => response.json())
                .then((data : IEvent[]) => {
                    let es: IEvent[];
                    if(selectedCategory === "Category" || selectedCategory === "")
                        es = data;
                    else
                        es = data.filter(event =>
                            event.category === selectedCategory
                        );

                    setEvents(es)
                })
                .catch()
        }
        else{
            defaultFetch()
        }
    }

    const defaultFetch = () => {
        fetch('http://localhost:5175/api/Event',{
            credentials: 'include'
        })
            .then((response) => response.json())
            .then((data : IEvent[]) => setEvents(data))
            .catch(() => navigate('/register'));
    }

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(event.target.value);
    };

    const handleLogOut = () => {
        Cookies.remove("cook");
        Cookies.remove("email");
    }


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
                        onChange={(e) => {
                            setSearchTerm("")
                            setSelectedCategory(e.target.value)
                        }}
                        className="filter-select"
                    >
                        <option value="">Category</option>
                        <option value="Concert">Concert</option>
                        <option value="Football">Football</option>
                        <option value="Art Gallery">Art Gallery</option>
                    </select>
                </div>
                <h2 onClick={handleLogOut}><Link className={"Link"} to="/login">Log out</Link></h2>
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
                            navigate(`/admin/event/${event.id}`)
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

export default StartAdmin