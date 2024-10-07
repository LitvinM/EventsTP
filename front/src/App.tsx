import React from 'react';
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import Start from "./Start";
import AddPage from "./AddPage";
import RegisterPage from "./RegisterPage";
import LoginPage from "./LoginPage";
import EventPage from "./EventPage";
import StartAdmin from "./StartAdmin";
import MyEvents from "./MyEvents";
import AdminEventPage from "./AdminEventPage";

export interface IEventRequest{
    name: string,
    description: string,
    timeAndDate: string,
    place: string,
    category: string,
    participantsMaxAmount: number,
    image: string
}


export interface IEvent{
    id: string,
    name: string,
    description: string,
    timeAndDate: string,
    place: string,
    category: string,
    participantsMaxAmount: number,
    participants: IParticipant[],
    image: string
}

export interface IParticipant {
    id: string,
    name: string,
    surname: string,
    dateOfBirth: string,
    email: string,
    password: string,
    events: IEvent[]
}

const App = () => {
      return(
          <BrowserRouter>
              <Routes>
                  <Route path="/" element={<Navigate to="/register"/>} />
                  <Route path="/home" element={<Start/>}/>
                  <Route path="/my-events" element={<MyEvents/>}/>
                  <Route path="/home-admin" element={<StartAdmin/>}/>
                  <Route path="/add-event" element={<AddPage/>}/>
                  <Route path="/event/:id" element={<EventPage/>}/>
                  <Route path="/admin/event/:id" element={<AdminEventPage/>}/>
                  <Route path="/register" element={<RegisterPage/>}/>
                  <Route path="/login" element={<LoginPage/>}/>
              </Routes>
          </BrowserRouter>
      )
};


export default App;