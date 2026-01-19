interface IconButtonProps {
    icon: string;
    text: string;
    onClick: () => void;
}

const IconButton = ({
    icon,
    text,
    onClick
}: IconButtonProps
) => {
    
    return (
        <div className="flex items-center justify-center w-1/2 rounded-full">
            <button className="bg-sky-500/100 w-full" onClick={onClick}>
                {text}
            </button>
        </div>
        
    );
}

export default IconButton;