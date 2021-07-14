import * as React from "react";
import jsonServerProvider from 'ra-data-json-server';
import { Admin, Resource, ListGuesser } from 'react-admin';

const dataProvider = jsonServerProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');
const App = () => (
      <Admin dataProvider={dataProvider}>
        <Resource name="Empleado" list={ListGuesser} />
          <Resource name="Nomina" list={ListGuesser} />
          <Resource name="Ingreso" list={ListGuesser} />
          <Resource name="Deduccion" list={ListGuesser} />
          <Resource name="Puesto" list={ListGuesser} />
          <Resource name="Departamento" list={ListGuesser} />
      </Admin>
  );

export default App;
