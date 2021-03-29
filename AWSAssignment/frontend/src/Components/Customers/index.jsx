import { React, useState, useEffect } from "react";
import { Row, Col, Table, Button, Card} from "react-bootstrap";
import { NavLink, withRouter } from 'react-router-dom';
import getAllCustomers from "./service";
import './style.css';

function Customers() {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    getAllCustomers()
      .then((resp) => {
        setCustomers(resp.data);
        document.getElementById('customer-table').DataTable();
      })
      .catch((err) => {
        console.log(err.response.data);
      });
  }, []);

  return (
    <>
      <Col xs="12">
        <NavLink
          to="/customer/add"
          className="btn add-btn float-right mb-4 mr-3 px-4"
        >
          Add
        </NavLink>
      </Col>
      <Col as={Row} xs="12">
        {
          customers ? customers.map(customer => {
            return (
              <Card as={Col} xs="12" lg="12" className="customer-card m-3">
                <Card.Img
                  variant="top"
                  src={customer.thumbnail}
                  className="img-rounded"
                  width="200"
                  height="200"
                />
                <Card.Body>
                  <Card.Title>{customer.name}</Card.Title>
                  <Card.Text>
                    <div> Email : {customer.email} </div>
                    <div> Contact : {customer.contactnumber} </div>
                    <div> Status : {customer.status === 0 ? 'Inactive' : 'Active'} </div>
                  </Card.Text>
                  <NavLink  to={`/customers/${customer.id}`} className="btn details-btn">Details</NavLink>
                </Card.Body>
              </Card>
            )
          }) : <h3 className="w-100 text-center">There are no customers to show</h3>
        }
      </Col>
    </>
  );
}

export default withRouter(Customers);
