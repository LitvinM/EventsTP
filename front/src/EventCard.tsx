import {IEvent} from "./App";
import React from "react";

interface IEventCard {
    event: IEvent
    onClickHandle: () => void
}

const EventCard = ({event, onClickHandle} : IEventCard) => {
    const e = {...event};

    return (
        <div className={"card" + (e.participants.length >= e.participantsMaxAmount ? " red" : "")} onClick={onClickHandle}>
            <span>{e.name}</span>
            <span>{e.category}</span>
            <span>{e.place}</span>
            <span>{new Date(e.timeAndDate).toLocaleString()}</span>
            <span className={"max-amount"}>{e.participants.length}/{e.participantsMaxAmount}</span>
        </div>
    )
}

export default EventCard