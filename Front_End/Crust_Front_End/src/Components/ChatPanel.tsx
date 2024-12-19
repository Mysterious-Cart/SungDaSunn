import { Avatar, Card, CardContent, Collapse, Grid2, ListItem, ListItemAvatar, ListItemText, ListSubheader, Stack, Typography, TextField, Fab} from "@mui/material";
import { TransitionGroup } from 'react-transition-group';
import './ChatPanel.jsx.css'
import { useEffect, useState } from "react";

interface Messages{
    Message: String
}

function ChatPanel(Messages){

    const [messages, setMessages] = useState<Messages[]>([])

    useEffect(() => {

    })

    return <>
        <TransitionGroup>
            <Collapse>
                <ListItem className="ChatBubble" disablePadding dense={true}>
                    <ListItemAvatar sx={{width:"25px", height:"25px"}}>
                        <Avatar />
                    </ListItemAvatar>
                    {
                        messages.map((eachMessage) => 
                        (
                            <ListItemText 
                                primary={
                                    <p style={{height:"10px"}}>nth <span style={{fontSize:"8pt",color:"gray"}}> 28/9/2021</span></p>} 
                                    secondary={eachMessage.Message} />
                        ))
                    }
                    
                </ListItem>
            </Collapse>
        </TransitionGroup>

        <Stack direction={"row"} style={{position:"absolute", bottom:"20px", width:"50%", minWidth:"500px"}}>
            <TextField style={{width:"100%", marginRight:"20px"}} placeholder="Type messages." inputMode={"text"} />
        </Stack>
    </>
}

export default ChatPanel;