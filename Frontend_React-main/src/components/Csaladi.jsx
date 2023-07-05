import React from "react";
import Navbar from './menu/Navbarmenu.js';
import "../css/mainPage.css";
import { useEffect, useState } from "react";
import axios from 'axios';
import { NavLink } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import "../css/card.css"
import { Paginator } from 'primereact/paginator';
import "primereact/resources/themes/lara-light-indigo/theme.css";     
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";



export default function Scifi() 
{const [film, setFilm] = useState([]);
  const [isFetchPending, setFetchPending] = useState(false);
  const [first, setFirst] = useState(0);
  const [rows, setRows] = useState(12);
  const navigate = useNavigate();

  useEffect(() => {
      setFetchPending(true);
      (async() => {
          try {
              const films = await axios.get("https://localhost:5001/Film/ByCategory?Kategoria=Csaladi");
              setFilm(films.data);
          } catch (err) {
              console.log(err);
          } finally {
              setFetchPending(false);
          }
      })();
    }, []);
  

    if (window.loggedin) {
         return (
        <>
          <Navbar/>
          <br />
          <br />
          <br />
          <br />
          <br />
          <h1 className="bb-title">Családi</h1>
          <div className="p-5  m-auto text-center position content bg-ivory">
            {isFetchPending ? (
              <div className="spinner-border"></div>
            ) : (
              <div>
                {film.slice(first, first + rows).map((film) => (
                  <NavLink key={film.id} to={"/film/" + film.id}>
                    <div className="card col-sm-3 d-inline-block m-1 p-2">
                    
                    <img alt="Place_Of_Image" height={"300px"} width={"325px"} src={`data:image/jpg;base64,${film.filmlogo}`}/>
                    <span className="card__footer ">
                    <span>{film.cim}</span>
                    <span>{film.ertekeles} <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-star-fill" viewBox="0 0 16 16">
      <path d="M3.612 15.443c-.386.198-.824-.149-.746-.592l.83-4.73L.173 6.765c-.329-.314-.158-.888.283-.95l4.898-.696L7.538.792c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.282.95l-3.522 3.356.83 4.73c.078.443-.36.79-.746.592L8 13.187l-4.389 2.256z"/>
    </svg></span>
                    </span>
                    
    </div>
                    </NavLink>
                ))}
              </div>
            )}
          </div>
          <Paginator
           first={first}
           rows={rows}
           totalRecords={film.length}
           template="PageLinks"
           onPageChange={(e)=>{
            setFirst(e.first);
            setRows(e.rows)
          }
          } />
        </>
        );
        }

      else{
        navigate("/")
      }
}