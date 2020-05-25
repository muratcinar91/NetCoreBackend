using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager :IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);

            var newUser = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(newUser);
            return new SuccessDataResult<User>(newUser,Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userCheck==null)
            {
                return  new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userCheck.PasswordHash,userCheck.PasswordSalt))
            {
               return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userCheck,Messages.SuccessfulLogin);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByMail(email)!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var userClaims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, userClaims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }
    }
}
