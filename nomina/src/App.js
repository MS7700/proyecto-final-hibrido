import * as React from "react";
//import jsonServerProvider from 'ra-data-json-server';
import simpleRestProvider from 'ra-data-simple-rest';
import { Admin, Resource, ListGuesser,EditGuesser } from 'react-admin';
import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';


const dataProvider = simpleRestProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');
const App = () => (
      <Admin dataProvider={dataProvider}>
        <Resource name="Empleados" list={EmpleadoList} edit={EmpleadoEdit} create={EmpleadoCreate} />
          <Resource name="Ingresos" list={IngresoList} edit={IngresoEdit} create={IngresoCreate}  />
          <Resource name="Deduccions" list={DeduccionList} edit={DeduccionEdit} create={DeduccionCreate}  />
          <Resource name="Puestos" list={PuestoList} edit={PuestoEdit} create={PuestoCreate} />
          <Resource name="Departamentos" list={DepartamentoList} edit={DepartamentoEdit} create={DepartamentoCreate} />
          <Resource name="Nominas" list={NominaList} edit={NominaEdit} create={NominaCreate} />
      </Admin>
  );

export default App;
