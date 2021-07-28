import { useEffect, useState } from "react";
//import jsonServerProvider from 'ra-data-json-server';
import simpleRestProvider from 'ra-data-simple-rest';
import odataProvider, { OdataDataProvider } from "ra-data-odata-server";
import { Admin, Resource, ListGuesser,EditGuesser } from 'react-admin';
import { IngresoList, IngresoEdit, IngresoCreate } from './entity/ingreso';
import { DeduccionList, DeduccionEdit, DeduccionCreate } from './entity/deduccion';
import { DepartamentoList, DepartamentoEdit, DepartamentoCreate } from './entity/departamento';
import { PuestoList, PuestoEdit, PuestoCreate } from './entity/puesto';
import { NominaList, NominaEdit, NominaCreate } from './entity/nomina';
import { EmpleadoList, EmpleadoEdit, EmpleadoCreate} from './entity/empleado';


const dataProvider = odataProvider('https://localhost:44340/odata/').then((p) => setDataProvider(p));
//const dataProvider = simpleRestProvider('http://localhost:55922/api');
//const dataProvider = simpleRestProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('http://localhost:55922/api');
//const dataProvider = jsonServerProvider('https://jsonplaceholder.typicode.com');

  /*
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
*/
//const dataProvider = useState<OdataDataProvider>({});


  const App = () => (
    <Admin dataProvider={dataProvider}>
      <Resource name="Empleado" list={ListGuesser} edit={EditGuesser}/>
        <Resource name="Puesto" list={ListGuesser} edit={EditGuesser} />
        <Resource name="Departamento" list={ListGuesser} edit={EditGuesser} />
        <Resource name="Nomina" list={ListGuesser} edit={EditGuesser} />
    </Admin>
);


/*
function App(){
  const [dataProvider, setDataProvider] = useState<OdataDataProvider>(() => {
    odataProvider(
      "https://services.odata.org/v4/Northwind/Northwind.svc/"
    ).then((p) => setDataProvider(p))
  });
    /*
  useEffect(() => {
    odataProvider(
      "https://services.odata.org/v4/Northwind/Northwind.svc/"
    ).then((p) => setDataProvider(p));
    return () => {};
  }, []);
  return(
    <Admin dataProvider={dataProvider}>
      <Resource name="Empleado" list={ListGuesser} edit={EditGuesser}/>
        <Resource name="Puesto" list={ListGuesser} edit={EditGuesser} />
        <Resource name="Departamento" list={ListGuesser} edit={EditGuesser} />
        <Resource name="Nomina" list={ListGuesser} edit={EditGuesser} />
    </Admin>
  );
}
*/

export default App;
