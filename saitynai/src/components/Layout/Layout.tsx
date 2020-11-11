import React, { ReactNode, useState } from 'react'
import { Link } from 'react-router-dom'
import ReactModal from 'react-modal'
import './Layout.css'
import menu from '../../icons/menu.svg'
import { CategoryList } from '../Category/CategoryList'
import { GetEmail, Logout } from '../../utils/user'
import { LoginForm } from '../User/Login'

interface IProps {
    children: ReactNode
}

export const Layout: React.FunctionComponent<IProps> = ({children}: IProps) => {
    const [isNavbarOpen, setIsNavbarOpen] = useState<boolean>(false)
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false)

    const login = () => {
        window.location.reload()
    }

    const logout = () => {
        Logout()
        window.location.reload()
    }

    return (
        <>
            <ReactModal
            isOpen={isModalOpen}
            overlayClassName="modalOverlay"
            className="modalBody"
            onRequestClose={() => setIsModalOpen(false)}
            >
                <LoginForm callback={() => login()}/>
            </ReactModal>
            <div className={"navbar navbarMobile" + (isNavbarOpen ? " navbarOpen" : "")}><CategoryList/></div>
            <div className={"body" + (isNavbarOpen ? " bodyOpen" : "")}>
                <header>
                    <button id="headerMenuButton" onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        <img alt="" src={menu}/>
                    </button>
                    <Link to="/" className="homeLink">Forum</Link>
                    { GetEmail() ?
                        <div>
                            {GetEmail()}
                        </div>
                        :
                        <div>
                            <button onClick={() => setIsModalOpen(true)}>
                                Login
                            </button>
                            <button onClick={() => setIsModalOpen(true)}>
                                Register
                            </button>
                        </div>
                    }
                </header>
                <div className="content">
                    <div className="navbarRegular">
                        <CategoryList/>
                    </div>
                    {children}
                </div>
                <footer>
                    Made by Gvidas Gaidauskas IFF-7/8
                    {GetEmail() ? <button onClick={() => logout()}>Logout</button> : null}
                </footer>
            </div>
        </>
    )
} 