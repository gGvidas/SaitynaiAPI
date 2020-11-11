import React, { ElementType } from 'react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import { routes } from './routes/routes'

type LayoutRouteProps = {
    layout: ElementType,
    component: ElementType,
    path: string,
    exact?: boolean
}

const LayoutRoute = ({layout, component, path, exact}: LayoutRouteProps) => {
    const Component = component
    const Layout = layout

    return <Route path={path} exact={exact}><Layout><Component/></Layout></Route>
}

export const App = () => 
<Router>
    <Switch>
        {routes.map(route => <LayoutRoute {...route} key={route.name}/>)}
    </Switch>
</Router>
