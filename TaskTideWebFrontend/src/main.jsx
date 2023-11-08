import React from 'react'
import ReactDOM from 'react-dom/client'
import Home from './routes/home'
import ErrorPage from "./routes/error-page"
import {
    createBrowserRouter,
    RouterProvider,
} from 'react-router-dom'

import './index.css'

const router = createBrowserRouter([
  {
    path: '/',
    element: <Home />,
    errorElement: <ErrorPage />
  },
])

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
)
