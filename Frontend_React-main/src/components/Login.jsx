import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { NotificationManager } from 'react-notifications';
import axios from "axios";
import "../css/loginxRegister.css";

export const Login = () => {

    const [username, setUname] = useState('');
    const [pass, setPass] = useState('');
    const [logged, setLogged] = useState(false);
    const[fullname, setFullName] = useState('');
    const[jogosultsag, setJogosultsag] = useState(0);
    const navigate = useNavigate();
    var sha256 = require('js-sha256');

    const handleSubmit = (e) => {
        e.preventDefault(); 
        (async () => {
          axios
          .post("https://localhost:5001/Login/SaltRequest/"+username)
          .then((response) => {
            let lekertSalt = response.data;
            console.log(lekertSalt)
            let tmpHash = sha256(pass + lekertSalt).toString();
            console.log(tmpHash)
            let url =
            "https://localhost:5001/Login?nev="+username+"&tmpHash="+tmpHash;
            axios
              .post(url)
              .then((response) => {
                if (response.status == 200) {
                  let tomb = response.data;
                  if (tomb[2] != -1) {
                    setLogged(true);
                    setUname(tomb[0]);
                    setFullName(tomb[1]);
                    setJogosultsag(tomb[2]);
                    setUname(username)
                    NotificationManager.success(username, "Sikeres bejelentkezés:");
                    window.loggedin=true;
                    navigate("/home")
                  } else {
                    NotificationManager.error("Sikertelen bejelentkezés!");
                  }
                } else {
                  //document.getElementById("uzenet").textContent =
                  //  "Paprikás krumpleee";
                }
              })
              .catch((error) => {
                alert(error);
              });
          })
          .catch((error) => {
            console.log(error);
            if (error.response.status == 400) {
              NotificationManager.error("Sikertelen bejelentkezés!");
            } else {
              alert("Üres a felhasználónév!");
            }
          });
        })();
    }

    return(
        <div className="bodyXd login-container position">
            <form className="login-form" onSubmit={handleSubmit}>
                
                <input value={username} onChange={(e) => setUname(e.target.value)} type="username" placeholder="Username" id="felhasznaloNev" name="felhasznaloNev" required/>
                <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="Password" id="Jelszo" name="Jelszo" required/>
                <button type="submit">Login</button>
            </form>
            <a className="link-btn" href="/register">Don't have an account? Register here!</a>
        </div>
    )
}