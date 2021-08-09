import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create,  SimpleForm, 
    NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput,DateField,
    DateInput, FormDataConsumer,RadioButtonGroupInput,Toolbar,SaveButton,TextInput,Show,SimpleShowLayout,BooleanInput,DeleteButton } from 'react-admin';

    const Filters = [
        <NumberInput label="ID" source="id" />,
        <TextInput label="Nombre"  source="Nombre" />,
        <DateInput source="Fecha" />,
        <TextInput label="Periodo"  source="Periodo" />,
        <ReferenceInput label="Tipo de Nómina" source="TipoNominaID" reference="TipoNomina" ><SelectInput label="Tipo de Nómina"   optionText="Descripcion" /></ReferenceInput>, 
        <BooleanInput source="Contabilizado" />
    ];


    export const NominaList = props => (
        <List {...props} filters={Filters} sort={{ field: 'Fecha', order: 'DESC' }}>
            <Datagrid rowClick="show">
                <TextField source="id" />
                <DateField source="Fecha" />
                <TextField source="Periodo" />
                <ReferenceField label="Tipo de Nómina" source="TipoNominaID" reference="TipoNomina"><TextField source="Descripcion" /></ReferenceField>
                <BooleanField source="Contabilizado" />
                <DeleteButton/>
            </Datagrid>
        </List>
    );

    export const NominaShow = props => (
        <Show {...props}>
            <SimpleShowLayout>
                <TextField source="id" />
                <DateField source="Fecha" />
                <TextField source="Periodo" />
                <ReferenceField label="Tipo de Nómina"  source="TipoNominaID" reference="TipoNomina"><TextField source="Descripcion" /></ReferenceField>
                <BooleanField source="Contabilizado" />
            </SimpleShowLayout>
        </Show>
    );

export const NominaCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <DateInput source="Fecha" />
            <ReferenceInput label="Tipo de Nómina" source="TipoNominaID" reference="TipoNomina" >
                <SelectInput label="Tipo de Nómina"   optionText="Descripcion" />
            </ReferenceInput>
        </SimpleForm>
    </Create>
);