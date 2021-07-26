import React from 'react'

export const Header = props => {
    return <nav className='navbar navbar-expand-lg navbar-light bg-light'>
        <div className='container-fluid'>
            
            <img className="navbar-brand" alt="Vontobel logo" src="/logo.png" width="40px"/>
            <div className="navbar-collapse" id="navbarSupportedContent">
                <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                    <li className="nav-item"><b>Vontobel Transaction Manager</b></li>
                </ul>
            </div>
        </div>
    </nav>
}