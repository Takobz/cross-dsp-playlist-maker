import { createRoot } from 'react-dom/client'
import './index.css'
import { RouterProvider } from 'react-router'
import routes from './app/routes.ts'
import { DSPAccessTokenContextProvider } from './app/context/DSPAccessTokenContextProvider.tsx'
import { DSPFromToSongsContextProvider } from './app/context/DSPFromToSongsContextProvider.tsx'
import Header from './app/ui/layout/header.tsx'

createRoot(document.getElementById('root')!).render(
   <div className='main-container'>
      <DSPAccessTokenContextProvider>
         <DSPFromToSongsContextProvider>
            <Header />
            <RouterProvider router={routes} />
         </DSPFromToSongsContextProvider>
      </DSPAccessTokenContextProvider>
   </div>
);
