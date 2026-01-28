import { createRoot } from 'react-dom/client'
import './index.css'
import { RouterProvider } from 'react-router'
import routes from './app/routes.ts'
import { DSPAccessTokenContextProvider } from './app/context/DSPAccessTokenContextProvider.tsx'

createRoot(document.getElementById('root')!).render(
   <div className='main-container'>
      <DSPAccessTokenContextProvider>
         <RouterProvider router={routes} />
      </DSPAccessTokenContextProvider>
   </div>
);
