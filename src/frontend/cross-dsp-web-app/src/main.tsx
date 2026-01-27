import { createRoot } from 'react-dom/client'
import './index.css'
import { RouterProvider } from 'react-router'
import routes from './app/routes.ts'

createRoot(document.getElementById('root')!).render(
   <div className='main-container'>
      <RouterProvider router={routes} />
   </div>
)
