import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';



export const EmpleadoList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Cedula" />
            <TextField source="Nombre" />
            <NumberField source="Salario" />
            <ReferenceField source="DepartamentoId" reference="Departamentos"><TextField source="id" /></ReferenceField>
            
        </Datagrid>
    </List>
);

export const EmpleadoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            
            <NumberInput source="Nomina.id" />
            <NumberInput source="Puesto.id" />
            <TextInput source="id" />
            <TextInput source="Cedula" />
            <TextInput source="Nombre" />
            <NumberInput source="Salario" />
                <SelectInput source="Departamento" optionText="departamento" />
        </SimpleForm>
    </Edit>
);