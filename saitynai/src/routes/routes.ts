import { ElementType } from "react";
import { AdminCategoryList } from "../components/Category/AdminCategoryList";
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
        name: 'Admin category list',
        component: AdminCategoryList,
        path: '/categories',
        layout: Layout,
        exact: true
    },
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