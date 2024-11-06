import './App.css'
import { ApolloClient, InMemoryCache, ApolloProvider, HttpLink, from} from '@apollo/client'
import {GetUser} from ""
const link = from([
  new HttpLink({uri: "http://156.67.216.145:90/graphql/"}),
]);

const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: link,
});

function App() {
  return <ApolloProvider client={client}>
    <body>

    </body>
  </ApolloProvider>
}

export default App
