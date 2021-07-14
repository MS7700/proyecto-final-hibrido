import * as React from "react";
import jsonServerProvider from 'ra-data-json-server';
import { Admin, Resource, ListGuesser,EditGuesser } from 'react-admin';
import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList/*, NominaEdit, NominaCreate */} from './entity/empleado';

const dataProvider = jsonServerProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');
const App = () => (
      <Admin dataProvider={dataProvider}>
        <Resource name="Empleado" list={ListGuesser} />
          <Resource name="Ingreso" list={IngresoList} edit={IngresoEdit} create={IngresoCreate}  />
          <Resource name="Deduccion" list={DeduccionList} edit={DeduccionEdit} create={DeduccionCreate}  />
          <Resource name="Puesto" list={PuestoList} edit={PuestoEdit} create={PuestoCreate} />
          <Resource name="Departamento" list={DepartamentoList} edit={DepartamentoEdit} create={DepartamentoCreate} />
          <Resource name="Nomina" list={NominaList} edit={NominaEdit} create={NominaCreate} />
      </Admin>
  );

export default App;
