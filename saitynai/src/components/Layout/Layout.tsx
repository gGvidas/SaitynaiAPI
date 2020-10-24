import React, { ReactNode, useState } from 'react'
import './Layout.css'

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
                    <button onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        abc
                    </button>
                </header>
                <div className="content">{abc()}</div>
                <footer>abc</footer>
            </div>
        </>
    )
} 