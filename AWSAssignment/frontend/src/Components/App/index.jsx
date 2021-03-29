import { Container, Row, Col } from 'react-bootstrap';
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom';
import Customers from '../Customers/index';
import AddCustomer from '../Customers/AddCustomer/index';
import CustomerDetails from '../CustomerDetails/index';
import { Toaster } from 'react-hot-toast';
import './style.css';

function App(props) {
  return (
    <Container>
      <Toaster />
      <Row as="h2" className="header p-3 mt-5 mb-3">
        <Col className="heading text-center p-2">
          Customer Management System
        </Col>
      </Row>
      <Row className="content p-3 m-0">
        <BrowserRouter>
          <Switch>
            <Route exact path='/'>
              <Customers />
            </Route>
            <Route path='/customers/:id' component={CustomerDetails} />
              {/* <CustomerDetails /> */}
            {/* </Route> */}
            <Route exact path='/customer/add'>
              <AddCustomer />
            </Route>
          </Switch>
        </BrowserRouter>
      </Row>
    </Container>
  );
}

export default App;
