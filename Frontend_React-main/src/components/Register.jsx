import React, { useState } from "react";
import axios from "axios";
import { NotificationManager } from 'react-notifications';
import "../css/loginxRegister.css";

export const Register = () => {

  const [email, setEmail] = useState('');
  const [pass, setPass] = useState('');
  const [name, setName] = useState('');
  const [uname, setUname] = useState('');
  var sha256 = require('js-sha256');

  function generateSalt(lengt) {
    var text = "";
    var possible =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < lengt; i++)
      text += possible.charAt(Math.floor(Math.random() * possible.length));
    return text;
  }

  const handleSubmit = (e) => {
    let userSalt=generateSalt(64);
      e.preventDefault();
          (async () => {
            try {
             axios.post("https://localhost:5001/Registry", {
                  Id: e.target.elements.Id?.value,
                  FelhasznaloNev: e.target.elements.FelhasznaloNev?.value,
                  TeljesNev: e.target.elements.TeljesNev?.value,
                  SALT: userSalt,
                  HASH: sha256(sha256(e.target.elements.Password?.value + userSalt).toString()).toString(),
                  Email: e.target.elements.Email?.value,
                  Key: "",
              }, );
              NotificationManager.success('Sikeres regisztráció!');
            } catch (err) {
              console.log(err);
            }
          })();
      e.preventDefault();
      console.log(email);
  } 

    
   

    return(
        <div className="bodyXd position register-container">
            <form className="register-form" onSubmit={handleSubmit}>
                <input value={name} onChange={(e) => setName(e.target.value)} type="text" placeholder="Fullname" id="fullname" name="TeljesNev" required/>
                <input value={uname} onChange={(e) => setUname(e.target.value)} type="text" placeholder="Username" id="name" name="FelhasznaloNev" required/>
                <input value={email} onChange={(e) => setEmail(e.target.value)} type="Email" placeholder="xmpl@youremail.com" id="email" name="Email" required/>
                <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="Password" id="password" name="Password" required/>
                <button type="submit">Register</button>
            </form>
            <a className="link-btn2" href="/">Already have an account? Login here!</a>
        </div>
    )
}