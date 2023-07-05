import React,{useState} from 'react';
import {NavLink, Link} from 'react-router-dom';
import { NotificationManager } from 'react-notifications';
import {FiAlignRight,FiXCircle,FiChevronDown } from "react-icons/fi";
import "../navbar.css"


const Navbarmenu = () => {

    const [isMenu, setisMenu] = useState(false);
    const [isResponsiveclose, setResponsiveclose] = useState(false);
    const toggleClass = () => {
      setisMenu(isMenu === false ? true : false);
      setResponsiveclose(isResponsiveclose === false ? true : false);
  };

  const torlesfuggveny = () => {
    setisMenu(isMenu === false ? true : false);
    setResponsiveclose(isResponsiveclose === false ? true : false);
    NotificationManager.success("Sikeres kijelentkezés!");
};

    let boxClass = ["main-menu menu-right menuq1"];
    if(isMenu) {
        boxClass.push('menuq2');
    }else{
        boxClass.push('');
    }

    const [isMenuSubMenu, setMenuSubMenu] = useState(false);
      
    const toggleSubmenu = () => {
      setMenuSubMenu(isMenuSubMenu === false ? true : false);
    };
    
    let boxClassSubMenu = ["sub__menus"];
    if(isMenuSubMenu) {
        boxClassSubMenu.push('sub__menus__Active');
    }else {
        boxClassSubMenu.push('');
    }

   

    return (
    <header className="header__middle">
        <div className="container">
            <div className="row">

                {/* Add Logo  */}
                <div className="header__middle__logo">
                    <NavLink  className="menu-item" to="/home">
                    <   label className="logo">Filmezz</label>
                    </NavLink>
                </div>

                <div className="header__middle__menus">
                    <nav className="main-nav " >
                    {/* Responsive Menu Button */}
                    {isResponsiveclose === true ? <> 
                        <span className="menubar__button" style={{ display: 'none' }} onClick={toggleClass} > <FiXCircle />   </span>
                    </> : <> 
                        <span className="menubar__button" style={{ display: 'none' }} onClick={toggleClass} > <FiAlignRight />   </span>
                    </>}
                    <ul className={boxClass.join(' ')}>
                    <li  className="menu-item" >
                        <NavLink className="menu-item" onClick={toggleClass} to={`/home`}> Főoldal </NavLink> 
                    </li>
                    <li onClick={toggleSubmenu} className="menu-item sub__menus__arrows" > <Link to="#"> Kategóriák <FiChevronDown /> </Link>
                        <ul className={boxClassSubMenu.join(' ')} > 
                            <li> <NavLink onClick={toggleClass} className="menu-item"  to={`/Akcio`}> Akció </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Animacio`}>  Animáció </NavLink> </li>
                            <li> <NavLink onClick={toggleClass} className="menu-item"  to={`/Csaladi`}> Családi </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Drama`}> Dráma </NavLink> </li>
                            <li> <NavLink onClick={toggleClass} className="menu-item"  to={`/Eletrajzi`}> Életrajzi </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Fantasy`}> Fantasy </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Horror`}> Horror </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Kaland`}> Kaland </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Krimi`}> Krimi </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Romantikus`}> Romantikus </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Scifi`}> Sci-fi </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Thriller`}> Thriller </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Tortenelmi`}> Történelmi </NavLink> </li>
                            <li><NavLink onClick={toggleClass} className="menu-item" to={`/Vigjatek`}> Vígjáték </NavLink> </li>
                        </ul>
                    </li>
                    <li className="menu-item " ><NavLink onClick={toggleClass} className="menu-item" to={`/Rolunk`}> Rólunk </NavLink> </li>
                    <li className="menu-item " ><NavLink onClick={torlesfuggveny} className="menu-item" to={`/`}> Kijelentkezés </NavLink> </li>
                    </ul>
                    </nav>     
                </div>   
            </div>
        </div>
    </header>
    )
}

export default Navbarmenu