import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField } from 'react-admin';


export const EmpleadoList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Cedula" />
            <TextField source="Nombre" />
            <NumberField source="Salario" />
            <TextField source="departamento.departamento" />
            <TextField source="puesto.puesto" />
            <TextField source="nomina.nomina" />
        </Datagrid>
    </List>
);