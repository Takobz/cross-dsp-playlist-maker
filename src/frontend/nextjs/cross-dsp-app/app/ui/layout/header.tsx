import './header.css'
import { Bars3Icon } from '@heroicons/react/24/outline';

const Header = () => {
    return (
        <header className='header-position'>
            <div className="header-content">
                <Bars3Icon className='clickable item' />
                <h2 className='clickable'>Cross DSP</h2>
                <h2 className='clickable item'>Login</h2>
            </div>
        </header>
    );    
}

export default Header;