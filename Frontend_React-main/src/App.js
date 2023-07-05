import React, { useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import 'react-notifications/lib/notifications.css';
import { NotificationContainer } from 'react-notifications';
import {Login} from "./components/Login";
import {Register} from "./components/Register";
import Home from "./components/HomePage";
import Akcio from "./components/Akcio";
import Scifi from "./components/Sci-fi";
import Animacio from "./components/Animacio";
import Csaladi from "./components/Csaladi";
import Drama from "./components/Drama";
import Eletrajzi from "./components/Eletrajzi";
import Fantasy from "./components/Fantasy";
import Horror from "./components/Horror";
import Kaland from "./components/Kaland";
import Krimi from "./components/Krimi";
import Romantikus from "./components/Romantikus";
import Thriller from "./components/Thriller";
import Tortenelmi from "./components/Tortenelmi";
import Vigjatek from "./components/Vigjatek";
import OneByOne from "./components/OneByOne";
import Rolunk from "./components/Rolunk";


function App() {


    window.loggedin = false;

    return (
        <BrowserRouter>
          <Routes>
              <Route path="/" element={<Login />}/>
              <Route path="/register" element={<Register />}/>
              <Route path="/film/:id" element={<OneByOne />}/>
              <Route path="/home" element={<Home />}/>
              <Route path="/Akcio" element={<Akcio />}/>
              <Route path="/Scifi" element={<Scifi />}/>
              <Route path="/Vigjatek" element={<Vigjatek />}/>
              <Route path="/Tortenelmi" element={<Tortenelmi />}/>
              <Route path="/Thriller" element={<Thriller />}/>
              <Route path="/Romantikus" element={<Romantikus />}/>
              <Route path="/Krimi" element={<Krimi />}/>
              <Route path="/Kaland" element={<Kaland />}/>
              <Route path="/Horror" element={<Horror />}/>
              <Route path="/Fantasy" element={<Fantasy />}/>
              <Route path="/Eletrajzi" element={<Eletrajzi />}/>
              <Route path="/Drama" element={<Drama />}/>
              <Route path="/Csaladi" element={<Csaladi />}/>
              <Route path="/Animacio" element={<Animacio />}/>
              <Route path="/Rolunk" element={<Rolunk />}/>
          </Routes>
          <NotificationContainer />
      </BrowserRouter>
    );
}
export default App;
