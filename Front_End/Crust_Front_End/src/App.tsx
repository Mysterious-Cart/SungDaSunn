import './App.css'
import { ApolloClient, InMemoryCache, ApolloProvider, HttpLink, from} from '@apollo/client'
import MainPage from './Pages/MainPage';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';
import PathManager from './Pages/PathManager';

const link = from([
  new HttpLink({uri: "http://156.67.216.145:5276/graphql/"}),
]);

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary:{
      main: '#31363F',
      light: '#76ABAE',
      dark: '#222831'
    },
    
  },
});

const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: link,
});

function App() {
  return (
    <ThemeProvider theme={darkTheme} >
      <CssBaseline/>
      <ApolloProvider client={client}>
        <BrowserRouter>
          <Routes>
            {/*Route indicate the url that take to a specific Page*/
              /*
              Example:
                <Route path="/Login" element={<LoginPage/>}/>
                In this case if the user put Localhost:5000/Login it will take them to LoginPage.tsx
              */
            }
            <Route index element={<MainPage/>}/>
            <Route path=':/' element={<MainPage/>}/>
            <Route path=':/Home' element={<MainPage/>}/>
            <Route path='/pathManager' element={<PathManager/>}/>
          </Routes>
        </BrowserRouter>
        

      </ApolloProvider>
    </ThemeProvider>
  )
}

export default App
