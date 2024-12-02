import { useEffect, useState } from "react";
import useGetUser from "./Connection/GetUser";
import './UserProfileList.jsx.css'
import ImageIcon from '@mui/icons-material/Image';
import { TransitionGroup } from 'react-transition-group';
import { Card, Stack, TextField, Paper, ListItem, Grid, List, ListItemText, Typography, Avatar, Grid2, Skeleton, ListItemIcon, Collapse, Badge, styled } from "@mui/material";
import { QueryManager } from "@apollo/client/core/QueryManager";
import { data } from "react-router";

interface IUser{
    name: string;
    user: [];
};

function UserProfileList(UserData:object){

    const [user, setUser] = useState<IUser[]>([])
    const [dUser, setdUser] = useState<IUser[]>([])
    const [FilterText, SetFilter] = useState("")
    const [isLoading, setIsLoading] = useState(true)
    const GetUser = useGetUser();

    useEffect(() => {

        const getUser = async() => await GetUser;

        if(GetUser){
            setUser(GetUser.user);
        }else{
            getUser();
        }

        if( user === undefined){
            setIsLoading(true)
            getUser();
        }else{
            setdUser(user.filter(user => user.name.toLowerCase().includes(FilterText.toLowerCase())))
            setIsLoading(false)
        }
        
    },[GetUser,user, dUser])

    return <div>
        <div style={{backgroundColor:"transparent"}}>
            <Stack style={{padding:"10px"}} gap={"10px"}>
                <TextField fullWidth={true} size="small" placeholder="Search..." onChange={(e) => SetFilter(e.target.value)} value={FilterText}/>
                <Typography variant={"caption"} color={"textSecondary"} marginLeft={"10px"}>
                    Direct Message
                </Typography>
            </Stack>
            <List>
                <TransitionGroup>
                {
                    isLoading == false?
                    dUser.map((EachUser,index) => (
                        <Collapse key={index}>
                            <ListItem  style={{borderRadius:"3px"}} className="User">
                                <ListItemIcon>
                                    <Badge badgeContent={6} invisible={false} slotProps={{badge:{className: "badge", style:{backgroundColor:"rgb(236, 100, 75)"}}}}>
                                        <Avatar>
                                            <ImageIcon/>
                                        </Avatar>
                                    </Badge>
                                </ListItemIcon>
                                <ListItemText primary={EachUser.name} secondary={"loading.."}></ListItemText>
                            </ListItem>
                        </Collapse>
                        
                    ))
                    :

                    <Collapse key={1}>
                        <ListItem  style={{borderRadius:"10px"}} className="User">
                            <ListItemIcon>
                            <Badge badgeContent={"?"} invisible={false} slotProps={{badge:{className: "badge", style:{backgroundColor:"rgb(236, 100, 75)"}}}}>
                                <Avatar></Avatar>
                            </Badge>
                            </ListItemIcon>
                            <ListItemText primary={<Skeleton/>} secondary={<Skeleton width={"90%"}/>}></ListItemText>
                        </ListItem>
                    </Collapse>

                    
                }
                </TransitionGroup>
            </List>
        </div>
    </div>


}



export default UserProfileList;

