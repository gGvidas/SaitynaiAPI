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
            <div className={"navbar" + (isNavbarOpen ? " navbar-open" : "")}>abcdef</div>
            <div className={"body" + (isNavbarOpen ? " body-open" : "")}>
                <header>
                <button id="header-menu-button" onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        <img src={menu}/>
                    </button>
                    Forum
                    <button>
                        <img src={user}/>
                    </button>
                </header>
                <div className="content">{abc()}</div>
                <footer>abc</footer>
            </div>
        </>
    )
} 