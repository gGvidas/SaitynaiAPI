import React, { ReactNode, useState } from 'react'
import './Layout.css'
import menu from '../../icons/menu.svg'
import user from '../../icons/user.svg'

interface IProps {
    children: ReactNode
}

export const Layout = ({children}: IProps) => {
    const [isNavbarOpen, setIsNavbarOpen] = useState(false)

    const abc = () => {
        const abc = []
        for (let index = 0; index < 80; index++) {
            abc.push(children)
        }
        return abc
    }

    return (
        <>
            <div className={"navbar navbarMobile" + (isNavbarOpen ? " navbarOpen" : "")}>abcdef</div>
            <div className={"body" + (isNavbarOpen ? " bodyOpen" : "")}>
                <header>
                    <button id="headerMenuButton" onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        <img src={menu}/>
                    </button>
                    Forum
                    <button>
                        <img src={user}/>
                    </button>
                </header>
                <div className="content">
                    <div className="navbarRegular">
                        abc
                    </div>
                    {abc()}
                </div>
                <footer>Made by Gvidas Gaidauskas IFF-7/8</footer>
            </div>
        </>
    )
} 