import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FaCartPlus } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import {FaCartArrowDown} from "react-icons/fa";
import { Route, Routes } from "react-router-dom";
import "../css/singleCard.scss"
import Navbar from './menu/Navbarmenu.js';


function OneByOne() {
    
    const param = useParams();
    const id = param.id;
    const [film, setFilm] = useState([{}]);
    const [isPending, setPending] = useState(false);
    console.log(param);
    const navigate = useNavigate();
    
    useEffect(() => {
        setPending(true);
          (async () => {
            try {
              const films = await axios.get("https://localhost:5001/Film/stepbystep?Id="+id);
              setFilm(films.data);
              console.log(films.data[0].logo);
            } catch (err) {
              console.log(err);
            } finally {
              setPending(false);
            }
          })();
      }, []);

    if (window.loggedin) {
    return (
      <>
      <Navbar/>
      <div className="kartyapozicio">
  
  <div className="wrapper">
    <div className="clash-card barbarian">
      <div className="clash-card__image clash-card__image--barbarian">
        <img className="filmmeret" src={`data:image/jpg;base64,${film[0].filmlogo}`} alt="barbarian" />
      </div>
      <div className="clash-card__level clash-card__level--barbarian">{film[0].kategoria}</div>
      <div className="clash-card__unit-name">{film[0].cim}</div>
      <div className="clash-card__unit-description">
        {film[0].leiras}
      </div>

      <div className="clash-card__unit-stats clash-card__unit-stats--barbarian clearfix">
        <div className="ertekelesbeallitas">
          <div className="stat">{film[0].ertekeles}<sup><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-star-fill" viewBox="0 0 16 16">
  <path d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"/>
</svg></sup></div>
          <div className="stat-value">Értékelés</div>
        </div>

      </div>

    </div>
  </div> 
  </div>
      </>
       
      ); }

      else{
        navigate("/")
      }
  }
  export default OneByOne;