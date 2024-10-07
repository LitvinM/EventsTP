import React from "react";
import {Link} from "react-router-dom";

const Header = () => {
    return (
        <div className={"header"}>
            <div/>
                <h1><Link className={"Link"} to="/home">Home</Link></h1>
                <h2><Link className={"Link"} to="/my-events">My Events</Link></h2>
            <div/>
        </div>
    )
}

export default Header;