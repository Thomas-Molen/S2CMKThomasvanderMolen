<div class="box box-info padding-1">
    <div class="box-body">

        <div class="form-group">
            {{ Form::label('username') }}
            {{ Form::text('username', $user->username, ['class' => 'form-control' . ($errors->has('username') ? ' is-invalid' : ''), 'placeholder' => 'Username']) }}
            {!! $errors->first('username', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('unique_key') }}
            {{ Form::text('unique_key', $user->unique_key, ['class' => 'form-control' . ($errors->has('unique_key') ? ' is-invalid' : ''), 'placeholder' => 'Unique Key']) }}
            {!! $errors->first('unique_key', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('admin') }}
            {{ Form::text('admin', $user->admin, ['class' => 'form-control' . ($errors->has('admin') ? ' is-invalid' : ''), 'placeholder' => 'admin 1 or 0']) }}
            {!! $errors->first('admin', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('password') }}
            {{ Form::text('password', "", ['class' => 'form-control' . ($errors->has('password') ? ' is-invalid' : ''), 'placeholder' => 'New Password']) }}
            {!! $errors->first('role_id', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('active') }}
            {{ Form::text('active', $user->active, ['class' => 'form-control' . ($errors->has('active') ? ' is-invalid' : ''), 'placeholder' => 'active 1 or 0']) }}
            {!! $errors->first('active', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="box-footer mt20">
            <button type="submit" class="btn btn-primary">Submit</button>
        </div>
    </div>
</div>
