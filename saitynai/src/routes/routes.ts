import { ElementType } from "react";
import { Layout } from "../components/Layout/Layout";
import { Thread } from "../components/Thread/Thread";
import { ThreadList } from "../components/Thread/ThreadList";

type Route = {
    name: string,
    component: ElementType,
    path: string,
    layout: ElementType,
    exact?: boolean
}

export const routes: Route[] = [
    {
        name: 'Thread',
        component: Thread,
        path: '/thread/:id',
        layout: Layout,
        exact: true
    },
    {
        name: 'Threads',
        component: ThreadList,
        path: '/:categoryId?',
        layout: Layout
    }
]