import React, { ReactNode, useState } from 'react'
import './Layout.css'
import menu from '../../icons/menu.svg'
import user from '../../icons/user.svg'
import { CategoryList } from '../Category/CategoryList'
import { GetEmail } from '../../utils/user'

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
            <div className={"navbar navbarMobile" + (isNavbarOpen ? " navbarOpen" : "")}><CategoryList/></div>
            <div className={"body" + (isNavbarOpen ? " bodyOpen" : "")}>
                <header>
                    <button id="headerMenuButton" onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        <img alt="" src={menu}/>
                    </button>
                    Forum
                    { GetEmail() ?
                        <button>
                            <img alt="" src={user}/>
                        </button>
                        :
                        <div>
                            <button>
                                Login
                            </button>
                            <button>
                                Register
                            </button>
                        </div>
                    }
                </header>
                <div className="content">
                    <div className="navbarRegular">
                        <CategoryList/>
                    </div>
                    {abc()}
                </div>
                <footer>Made by Gvidas Gaidauskas IFF-7/8</footer>
            </div>
        </>
    )
} 