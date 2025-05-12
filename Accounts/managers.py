from django.contrib.auth.models import BaseUserManager


# class UserManager(BaseUserManager):
#     def create_user(self,national_code,password):
#         if not national_code:
#             raise ValueError('ورود کد ملی الزامیست')
        
#         user=self.model(national_code=national_code)
#         user.set_password()
#         user.save(using=self._db)
#         return user
    

#     def create_superuser(self,national_code,password):
#         if not national_code:
#             raise ValueError('ورود کد ملی الزامیست')
        
#         user=self.model(national_code=national_code)
#         user.set_password(password)
#         user.is_admin=True
#         user.is_staff=True
#         user.save(using=self._db)
#         return user
class UserManager(BaseUserManager):
    def create_user(self, national_code,first_name,last_name, password=None, **extra_fields):
        if not national_code:
            raise ValueError("کد ملی باید وارد شود")
        
        user = self.model(national_code,first_name,last_name,**extra_fields)
        user.save(using=self._db)
        return user

    def create_superuser(self, national_code, password=None, **extra_fields):
        extra_fields.setdefault('is_staff', True)
        extra_fields.setdefault('is_superuser', True)

        if extra_fields.get('is_staff') is not True:
            raise ValueError("Superuser must have is_staff=True.")
        if extra_fields.get('is_superuser') is not True:
            raise ValueError("Superuser must have is_superuser=True.")

        return self.create_user(national_code, password, **extra_fields)