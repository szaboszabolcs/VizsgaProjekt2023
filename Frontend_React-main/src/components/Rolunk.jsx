import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/CardContainer.css';
import Navbar from './menu/Navbarmenu.js';
import Balazs from "../images/Balazs.jpg";
import Puda from "../images/Puda.jpg";
import Szabi from "../images/Szabi.jpg"

export default function Rolunk(){
  const [wait, setWait] = useState(false);
  const navigate = useNavigate();
  useEffect(() => {
    setWait(true);
}, []);
if (window.loggedin) {
const Card = ({ name, age, imageSrc, telefonszam, email }) => {
  return (
    <div className="kartya hatter">
      <img src={imageSrc} alt={`Picture of ${name}`} />
      <div className="kartya-info">
        <h2 className='centerezes'>{name}({age})</h2>
        <p>Tel.: {telefonszam}</p>
        <p>E-mail: {email}</p>
        {/* Additional information can be added here */}
      </div>
    </div>
  );
};

  const cardsData = [
    {
      name: 'Jakab Balázs Erik',
      age: 21,
      imageSrc: Balazs,
      telefonszam: "+36307525994",
      email: "jakabb1@kkszki.hu"
      /* Additional information can be added here */
    },
    {
      name: 'Puda Ádám',
      age: 22,
      imageSrc: Puda,
      telefonszam: "+36203959778",
      email: "pudaa@kkszki.hu"
      /* Additional information can be added here */
    },
    {
      name: 'Szabó Szabolcs',
      age: 23,
      imageSrc: Szabi,
      telefonszam: "+36705210829",
      email: "szabosz@kkszki.hu"

      /* Additional information can be added here */
    },
  ];

  return (
    <>
    <Navbar/>
    <br />
    <br />
    <br />
    <br />
    <br />
    

    <div className="card-container">
      {cardsData.map((cardData, index) => (
        <Card key={index} {...cardData} />
      ))}
    </div>
    </>
  );
}
else{
  navigate("/")
};
}