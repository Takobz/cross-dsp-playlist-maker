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
        <button onClick={onClick}>
            {text}
        </button>
    );
}

export default IconButton;